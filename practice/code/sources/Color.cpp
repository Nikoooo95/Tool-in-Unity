#include "Color.hpp"

namespace tool {

	Color::Color(float newR, float newG, float newB) :
		r(newR), g(newG), b(newB)
	{

	}

	Color::Color(const Color& other) :
		r(other.r), g(other.g), b(other.b)
	{

	}

	void Color::set(float newR, float newG, float newB)
		
	{
		r = newR;
		g = newG;
		b = newB;
	}

	Color Color::operator=(const Color & other)
	{
		r = other.r;
		g = other.g;
		b = other.b;
		return *this;
	}

}