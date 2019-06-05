#ifndef VECTOR3_HEADER
#define VECTOR3_HEADER

namespace tool
{

	struct Vector3
	{

	public:
		float x;
		float y;
		float z;

	public:
		Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f);

		Vector3(const Vector3& other);

		void set(float x_, float y_, float z_);

		Vector3 operator=(const Vector3 &);
	};
}

#endif // !VECTOR3_HEADER
