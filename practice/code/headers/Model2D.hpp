#ifndef MODEL_2D_HEADER
#define MODEL_2D_HEADER
#include "Model.hpp"
#include "Color.hpp"
namespace tool 
{
	class Model2D 
	{
	private:
		std::vector<std::shared_ptr<Vector2>> vertex;
		std::shared_ptr<Color> color;
		std::string name;

	public:
		Model2D(std::string _name) : name(_name){}

	public:
		void addVertex(std::shared_ptr<Vector2> vert);
		void fillVectors(Vector2 _vectors[]);

	public:
		inline const std::string getName() 
		{
			return name;
		}

		inline int getVectorsAmount()
		{
			return (int)vertex.size();
		}

		void setColor(std::shared_ptr<Color> newColor) {
			//color.reset(new std::shared_ptr<Color>(newColor));
		}


	};
}

#endif // !MODEL_2D_HEADER
