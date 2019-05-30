#include "pch.h"
#include "ProcGen.h"
#include <algorithm>
#include <ctime>

void ProcGen::ResetCells()
{
	if (cells != NULL) delete cells;
	int size = width * height;
	cells = new char[size];
	for (int i = 0; i < size; i++) {
		cells[i] = WALL_CELL;
	}
}

ProcGen::ProcGen()
{
	srand(std::time(NULL));
	ResetCells();
}


ProcGen::~ProcGen()
{
}

void ProcGen::SetWidth(int width)
{
	this->width = width;
	ResetCells();
}

void ProcGen::SetHeight(int height)
{
	this->height = height;
	ResetCells();
}

void ProcGen::SetRoomMinSize(int min)
{
	roomMinSize = min;
}

void ProcGen::SetRoomMaxSize(int max)
{
	roomMaxSize = max;
}

void ProcGen::SetMinRooms(int min)
{
	minRooms = min;
}

void ProcGen::SetMaxRooms(int max)
{
	maxRooms = max;
}

void ProcGen::Generate()
{
	ResetCells();
	
	int roomCount = rand() % (maxRooms - minRooms) + minRooms;
	int* roomCenterX = new int[roomCount];
	int* roomCenterY = new int[roomCount];

	for (int i = 0; i < roomCount; i++) {
		int roomWidth = rand() % (roomMaxSize - roomMinSize) + roomMinSize;
		int roomHeight = rand() % (roomMaxSize - roomMinSize) + roomMinSize;
		int roomX = rand() % (width - roomWidth);
		int roomY = rand() % (height - roomHeight);
		
		roomCenterX[i] = roomX + roomWidth / 2;
		roomCenterY[i] = roomY + roomHeight / 2;

		for (int x = 0; x < roomWidth; x++) {
			for (int y = 0; y < roomHeight; y++) {
				cells[(y + roomY) * width + (x + roomX)] = FLOOR_CELL;
			}
		}
	}

	for (int times = 0; times < 2; times++) {
		for (int i = 0; i < roomCount; i++) {
			int targetRoom = rand() % roomCount;

			int minX = std::min(roomCenterX[targetRoom], roomCenterX[i]);
			int minY = std::min(roomCenterY[targetRoom], roomCenterY[i]);
			int maxX = std::max(roomCenterX[targetRoom], roomCenterX[i]);
			int maxY = std::max(roomCenterY[targetRoom], roomCenterY[i]);
			
			if (rand() % 100 < 50) {
				for (int x = minX; x <= maxX; x++) {
					cells[minY * width + x] = FLOOR_CELL;
				}
				for (int y = minY; y <= maxY; y++) {
					cells[y * width + maxX] = FLOOR_CELL;
				}
			}
			else {
				for (int y = minY; y <= maxY; y++) {
					cells[y * width + minX] = FLOOR_CELL;
				}
				for (int x = minX; x <= maxX; x++) {
					cells[maxY * width + x] = FLOOR_CELL;
				}
			}
		}
	}

	delete roomCenterX;
	delete roomCenterY;
}

