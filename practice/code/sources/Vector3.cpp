#include "Vector3.hpp"

namespace tool 
{
	Vector3::Vector3(float x_, float y_, float z_) :
		x(x_), y(y_), z(z_)
	{}

	Vector3::Vector3(const Vector3& other) :
		x(other.x), y(other.y), z(other.z)
	{}

	void Vector3::set(float x_, float y_, float z_)
	{
		x = x_;
		y = y_;
		z = z_;
	}

	Vector3 Vector3::operator=(const Vector3 & other)
	{
		x = other.x;
		y = other.y;
		z = other.z;
		return *this;
	}


}