#ifndef VERTEX_2D_HEADER
#define VERTEX_2D_HEADER
#include "Vertex.hpp"
namespace tool 
{

	struct Vector2 
	{

	public:
		float x;
		float y;

	public:
		Vector2(float x = 0.0f, float y = 0.0f);

		Vector2(const Vector2& other);

		void set(float _x, float _y);

		Vector2 operator=(const Vector2 &);
	};
}

#endif // !VERTEX_2D_HEADER
