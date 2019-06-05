#ifndef COLOR_HEADER
#define COLOR_HEADER

namespace tool 
{

	struct Color 
	{
	public:
		float r;
		float g;
		float b;

	public:
		Color(float r = 0, float g = 0, float b = 0);
		Color(const Color& other);
		void set(float r, float g, float b);
		Color operator=(const Color &);

	
	};
}

#endif // !COLOR_HEADER
