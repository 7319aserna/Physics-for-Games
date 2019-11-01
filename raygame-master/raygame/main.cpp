#include "game.h"
#include "raylib.h"

int main()
{
	game application;
	application.init();
	application.targetFixedStep = 1.0f / 30.0f;// target physics tick rate

	// Main game loop
	while (!WindowShouldClose())    // Detect window close button or ESC key
	{
		application.tick();

		while (application.shouldPhysics()) { application.tickPhysics(); }

		application.draw();
	}
	application.exit();
	return 0;
}