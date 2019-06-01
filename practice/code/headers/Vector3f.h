

#ifndef VECTOR3F__HEADER
#define VECTOR3F__HEADER



namespace mathexp
{
	struct Vector3f
	{
	public:
		float x;
		float y;
		float z;

		Vector3f(float x = 0, float y = 0, float z = 0);

		Vector3f(const Vector3f&);

		bool equals(Vector3f other);

		void set(float new_x, float new_y, float new_z);

		Vector3f operator=(const Vector3f&);

		static Vector3f multiply(Vector3f a, Vector3f b);

		Vector3f operator *(const Vector3f&);

		//Se necesita quaternion
		//Vector3f rotate_around_point(Vector3f pivot, Vector3f point, )

	};
}
#endif
