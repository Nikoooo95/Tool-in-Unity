#include "Color.hpp"

namespace tool {

	Color::Color(int newR, int newG, int newB) :
		r(newR), g(newG), b(newB)
	{

	}

	Color::Color(const Color& other) :
		r(other.r), g(other.g), b(other.b)
	{

	}

	void Color::set(int newR, int newG, int newB)
		
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