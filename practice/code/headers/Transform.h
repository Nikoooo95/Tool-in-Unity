

#ifndef TRANSFORM__HEADER
#define TRANSFORM__HEADER


#include "Vector3f.h"

using namespace mathexp;

class Transform
{
public:
	Vector3f position;
	Vector3f rotation; //Quaternion mejor
	Vector3f scale;

	Transform() = default;

	Transform(Vector3f position, Vector3f rotation, Vector3f scale)
		:position(position), rotation(rotation), scale(scale)
	{
	}

	void set(Vector3f p, Vector3f r, Vector3f s)
	{
		position = p;
		rotation = r;
		scale = s;
	}

};
#endif