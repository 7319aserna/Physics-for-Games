#include "shapes.h"

#include "glm/glm/glm.hpp"

#include "physics.h"

bool checkCircleCircle(glm::vec2 positionA, circle circleA, glm::vec2 positionB, circle circleB)
{
	// Get the distance
	float distance = glm::length(positionA - positionB);
	// Get the sum of the radii
	float sum = circleA.radius + circleB.radius;

	return distance < sum;
}

bool checkAABBAABB(glm::vec2 positionA, aabb aabbA, glm::vec2 positionB, aabb aabbB)
{
	return positionA.x - aabbA.halfExtents.x < positionB.x + aabbB.halfExtents.x && // l r
		   positionA.x + aabbA.halfExtents.x > positionB.x - aabbB.halfExtents.x && // r l
		   positionA.y - aabbA.halfExtents.y < positionB.y + aabbB.halfExtents.y && // t b
		   positionA.y + aabbA.halfExtents.y > positionB.y - aabbB.halfExtents.y;	// b t
}

bool checkCircleAABB(glm::vec2 positionA, circle circ, glm::vec2 positionB, aabb ab)
{
	float distanceX = positionA.x - glm::clamp(positionA.x, positionB.x - ab.halfExtents.x, positionB.x + ab.halfExtents.x );
	float distanceY = positionA.y - glm::clamp(positionA.y, positionB.y - ab.halfExtents.y, positionB.y + ab.halfExtents.y );
	return (distanceX * distanceX + distanceY * distanceY) < (circ.radius * circ.radius);
}

bool checkCircleX(glm::vec2 positionA, circle lhs, glm::vec2 positionB, shape rhs)
{
	return rhs.match([positionA, lhs, positionB](circle s) {return checkCircleCircle(positionA, lhs, positionB, s); },
					 [positionA, lhs, positionB](aabb s)   {return checkCircleAABB(positionA, lhs, positionB, s); });
}

bool checkAABBX(glm::vec2 positionA, aabb lhs, glm::vec2 positionB, shape rhs)
{
	return rhs.match([positionA, lhs, positionB](circle s) { return checkCircleAABB(positionA, s, positionB, lhs); },
					 [positionA, lhs, positionB](aabb s)   { return checkAABBAABB(positionA, lhs, positionB, s); });
}

void resolvePhysicsBodies(physicsObject & lhs, physicsObject & rhs)
{
	glm::vec2 resImpulse[2];

	glm::vec2 normal = { 0,0 };
	float pen = 0.0f;

	normal = lhs.collider.match([lhs, rhs, &pen](circle) {
		float distance = glm::length(lhs.position - rhs.position);
		float sum = lhs.collider.get<circle>().radius + rhs.collider.get<circle>().radius;
	
		pen = sum - distance;

		return glm::normalize(lhs.position - rhs.position);
	},
		[lhs, rhs, &pen](aabb a) { assert(false && "not yet implemented"); return glm::vec2(); }
	);

	resolveCollision(lhs.position, lhs.velocity, lhs.mass, 
					 rhs.position, rhs.velocity, rhs.mass, 
					 1.0f, normal, resImpulse);

	lhs.position += normal * pen;
	rhs.position -= normal * pen;

	lhs.velocity = resImpulse[0];
	rhs.velocity = resImpulse[1];
}

void resolveCollision(glm::vec2 positionA, glm::vec2 velocityA, float massA, glm::vec2 positionB, glm::vec2 velocityB, float massB, float elasticity, glm::vec2 normal, glm::vec2 * distance)
{
	glm::vec2 relVel = velocityA - velocityB;
	float impulseMag = glm::dot(-(1.0f + elasticity) * relVel, normal) /
					   glm::dot(normal, normal * (1 / massA + 1 / massB));

	distance[0] = velocityA + (impulseMag / massA) * normal;
	distance[1] = velocityB - (impulseMag / massB) * normal;
}