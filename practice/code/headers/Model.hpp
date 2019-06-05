#ifndef MODEL_HEADER
#define MODEL_HEADER

#include <string>
#include <vector>

#include "Color.hpp"
#include "Vector2.hpp"
#include "Vector3.hpp"

namespace tool 
{
	class Model 
	{
	private:
		std::shared_ptr<Color> color;
		std::string name;
		float height;
		
	protected:
		Model(std::string name_, float height_): name(name_), height(height_) {}

	public:
		inline const std::string getName()
		{
			return name;
		}

		void setColor(std::shared_ptr<Color> newColor)
		{
			color = newColor;
		}

		void modifyColor(Color* color_)
		{
			color_->set(color->r, color->g, color->b);
		}

		std::shared_ptr<Color> getColor() 
		{
			return color;
		}

		void setHeight(float height_) {
			height = height_;
		}

		inline float getHeight() {
			return height;
		}

		virtual int getVectorsAmount() = 0;

	};
}
	


#endif // !MODEL_HEADER
