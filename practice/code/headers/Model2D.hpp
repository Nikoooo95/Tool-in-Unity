#ifndef MODEL_2D_HEADER
#define MODEL_2D_HEADER

#include "Model.hpp"

namespace tool 
{
	class Model2D : public Model
	{
	private:
		std::vector<std::shared_ptr<Vector2>> vertex;

	public:
		Model2D(std::string name_, float height_) : Model(name_, height_){}

	public:
		void addVertex(std::shared_ptr<Vector2> vert);
		void fillVectors(Vector2 _vectors[]);


	public:

		inline int getVectorsAmount() override
		{
			return (int)vertex.size();
		}

		std::shared_ptr<Vector2> getVector(int pos)
		{
			return vertex[pos];
		}


	};
}

#endif // !MODEL_2D_HEADER
