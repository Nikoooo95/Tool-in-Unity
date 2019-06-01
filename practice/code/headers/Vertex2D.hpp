#ifndef VERTEX_2D_HEADER
#define VERTEX_2D_HEADER
#include "Vertex.hpp"
namespace tool {
	class Vertex2D {
	private:
		float x;
		float y;
	public:
		Vertex2D(float _x = 0.0f, float _y = 0.0f) :
		x(_x), y(_y){}

	public:
		float getX() { return x; }
		float getY() { return y; }

		void setX(float _x) { x = _x; }
		void setY(float _y) { y = _y; }
		
		void reset() {
			x = 0;
			y = 0;
		}
	};
}

#endif // !VERTEX_2D_HEADER
