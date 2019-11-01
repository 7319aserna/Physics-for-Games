#pragma once

#include "physics.h"
#include <vector>

class game
{
private:
	float accumulatedFixedTime;

	std::vector<physicsObject> physicsObjects;

public:
	game();

	bool shouldClose() const;
	bool shouldPhysics() const;

	float targetFixedStep;

	void draw() const;
	void exit();
	void init();
	void tick();
	void tickPhysics();
};