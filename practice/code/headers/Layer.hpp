#ifndef LAYER_HEADER
#define LAYER_HEADER

#include <map>
#include <string>

#include "Model2D.hpp"
#include "Model3D.hpp"

namespace tool 
{
	class Layer 
	{

	private:
		std::string name;

	protected:
		Layer(std::string name_) : name(name_){}

	public:

		inline const std::string getName()
		{
			return name;
		}

	public:
		virtual void getColor(int model, Color* color) = 0;
		
	};
}

#endif // !LAYER_HEADER
