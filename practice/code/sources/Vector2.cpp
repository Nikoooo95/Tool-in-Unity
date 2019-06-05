#include "Vector2.hpp"

namespace tool 
{

	Vector2::Vector2(float x_, float y_) :
		x(x_), y(y_)
	{}

	Vector2::Vector2(const Vector2& other) :
		x(other.x), y(other.y)
	{}

	void Vector2::set(float x_, float y_)
	{
		x = x_;
		y = y_;
	}

	Vector2 Vector2::operator=(const Vector2 & other) 
	{
		x = other.x;
		y = other.y;
		return *this;
	}

}