

#ifndef VECTOR2F__HEADER
#define VECTOR2F__HEADER



namespace mathexp
{
	struct Vector2f
	{
	public:
		float x;
		float y;

		Vector2f(float x = 0, float y = 0);

		Vector2f(const Vector2f&);

		bool equals(Vector2f other);

		void set(float new_x, float new_y, float new_z);

		Vector2f operator=(const Vector2f&);

		static Vector2f multiply(Vector2f a, Vector2f b);

		Vector2f operator *(const Vector2f&);

	};
}
#endif