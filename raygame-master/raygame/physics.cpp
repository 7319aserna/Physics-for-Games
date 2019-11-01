#include "physics.h"
#include "raylib.h"

physicsObject::physicsObject() {
	forces = { 0,0 };
	position = { 0,0 };
	velocity = { 0,0 };

	drag = 1.0f;
	mass = 1.0f;
}

void physicsObject::addAcceleration(glm::vec2 acceleration) { forces += acceleration; }

void physicsObject::addForce(glm::vec2 force) { forces += force * (1.0f / mass); }

void physicsObject::addImpulse(glm::vec2 impulse) { velocity += impulse * (1.0f / mass); }

void physicsObject::addVelocityChange(glm::vec2 delta) { velocity += delta; }

void physicsObject::draw() const { 
	// Check for different inputs
	DrawCircleLines(position.x, position.y, 15.0f, RED); 
}

void physicsObject::tickPhysics(float delta)
{
	// Intergrating forces into velocity
	velocity += forces * delta;
	forces = { 0,0 };

	// Intergrate linear drag
	velocity *= 1.0f - delta * drag;

	// Intergrating velocity into position
	position += velocity * delta;
}
