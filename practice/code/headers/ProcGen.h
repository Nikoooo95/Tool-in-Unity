#pragma once
#include <stdlib.h>

class ProcGen
{
private:
	int width = 20;
	int height = 20;
	int roomMinSize = 3;
	int roomMaxSize = 4;
	int minRooms = 3;
	int maxRooms = 5;
	char* cells = NULL;

	const char WALL_CELL = 'X';
	const char FLOOR_CELL = ' ';

	void ResetCells();

public:
	ProcGen();
	~ProcGen();
	void SetWidth(int width);
	void SetHeight(int height);
	void SetRoomMinSize(int min);
	void SetRoomMaxSize(int max);
	void SetMinRooms(int min);
	void SetMaxRooms(int max);

	int GetWidth() { return width; }
	int GetHeight() { return height; }
	int GetRoomMinSize() { return roomMinSize; }
	int GetRoomMaxSize() { return roomMaxSize; }
	int GetMinRooms() { return minRooms; }
	int GetMaxRooms() { return maxRooms; }
	char* GetDungeon() { return cells; }

	void Generate();
};

