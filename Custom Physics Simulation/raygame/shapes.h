#pragma once
#include "glm/glm/vec2.hpp"
#include "mapbox/variant.hpp"

struct aabb
{
	glm::vec2 halfExtents;
};

struct circle
{
	float radius;
};

using shape = mapbox::util::variant<circle, aabb>;

bool checkCircleCircle(glm::vec2 positionA, circle circleA, glm::vec2 positionB, circle circleB);
bool checkAABBAABB(glm::vec2 positionA, aabb aabbA, glm::vec2 positionB, aabb aabbB);
bool checkCircleAABB(glm::vec2 positionA, circle circ, glm::vec2 positionB, aabb ab);

bool checkCircleX(glm::vec2 positionA, circle lhs, glm::vec2 positionB, shape rhs);
bool checkAABBX(glm::vec2 positionA, aabb lhs, glm::vec2 positionB, shape rhs);

void resolvePhysicsBodies(class physicsObject& lhs, class physicsObject& rhs);

void resolveCollision(glm::vec2 positionA, glm::vec2 velocityA, float massA,
					  glm::vec2 positionB, glm::vec2 velocityB, float massB,
					  float elasticity, glm::vec2 normal, glm::vec2* distance);