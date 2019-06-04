#ifndef COLOR_HEADER
#define COLOR_HEADER

namespace tool 
{

	struct Color 
	{
	public:
		int r;
		int g;
		int b;

	public:
		Color(int r = 0, int g = 0, int b = 0);
		Color(const Color& other);
		void set(int r, int g, int b);
		Color operator=(const Color &);

	
	};
}

#endif // !COLOR_HEADER
