#include "game.h"
#include "raylib.h"

#include <iostream>

game::game()
{
	accumulatedFixedTime = 0.0f;
	targetFixedStep = 1.0f / 30.0f;
}

bool game::shouldClose() const { return WindowShouldClose(); }

bool game::shouldPhysics() const { return accumulatedFixedTime >= targetFixedStep; }

void game::draw() const
{
	// Draw
	//----------------------------------------------------------------------------------
	BeginDrawing();

	ClearBackground(RAYWHITE);

	for (const auto& I : physicsObjects) { I.draw(); }

	EndDrawing();
	//----------------------------------------------------------------------------------
}

void game::init()
{
	// Initialization
	//--------------------------------------------------------------------------------------
	int screenWidth = 800;
	int screenHeight = 450;

	InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");

	SetTargetFPS(60);
	//--------------------------------------------------------------------------------------
}

void game::tick() { 
	accumulatedFixedTime += GetFrameTime(); 
	
	bool mb0 = IsMouseButtonPressed(0);
	bool mb1 = IsMouseButtonPressed(1);

	if (mb0 || mb1) {
		physicsObjects.emplace_back();
		std::cout << "Added a physics object!" << std::endl;

		auto& babyPhysics = physicsObjects[physicsObjects.size() - 1];
		auto mousePosition = GetMousePosition();
		babyPhysics.position = { mousePosition.x, mousePosition.y };
		
		if (mb0) { babyPhysics.collider = circle{ 15.0f }; }
		else { babyPhysics.collider = aabb{ {15, 15} }; };
		// babyPhysics.addForce({ 1000, 0 });
	}
}

void game::tickPhysics()
{
	accumulatedFixedTime -= targetFixedStep;

	// std::cout << "Physics Tick" << std::endl;
	for (auto& I : physicsObjects) {
		I.tickPhysics(targetFixedStep);
	}

	for (auto &S : physicsObjects) {
		for (auto &J : physicsObjects) 
		{
			if (&S == &J) { continue; }

			bool collision = false;

			S.collider.match([S, J, &collision](circle c) { if (checkCircleX(S.position, c, J.position, J.collider)) { collision = true; } },
							 [S, J, &collision](aabb a) {if (checkAABBX(S.position, a, J.position, J.collider)) { collision = true; }});
		
			if (collision) { resolvePhysicsBodies(S, J); }
		}
	}
}

void game::exit()
{
	// De-Initialization
	//--------------------------------------------------------------------------------------   
	CloseWindow();        // Close window and OpenGL context
	//--------------------------------------------------------------------------------------
}
