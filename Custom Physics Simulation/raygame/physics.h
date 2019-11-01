#pragma once

#include "glm/glm/vec2.hpp"
#include "shapes.h"

class physicsObject {
public:
	float drag;
	float mass;

	glm::vec2 forces;
	
	glm::vec2 position;
	glm::vec2 velocity;

	physicsObject();

	shape collider;

	// Accelerates the object w/o respect to mass
	void addAcceleration(glm::vec2 acceleration);

	// Add a continuous force with respect to mass
	void addForce(glm::vec2 force);
	
	// Add an instantaneous force with respect to mass
	void addImpulse(glm::vec2 impulse);
	
	// Adds an instantaneous force w/o respect to mass
	void addVelocityChange(glm::vec2 delta);
	void draw() const;
	void tickPhysics(float delta);
};