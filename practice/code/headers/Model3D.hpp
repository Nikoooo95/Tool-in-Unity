#ifndef MODEL_3D_HEADER
#define MODEL_3D_HEADER

#include "Model.hpp"


namespace tool
{
	class Model3D : public Model
	{
	private:
		std::vector<std::shared_ptr<Vector3>> vertex;

	public:
		Model3D(std::string name_, float height_) : Model(name_, height_){}

	public:
		void addVertex(std::shared_ptr<Vector3> vert);
		void fillVectors(Vector3 vectors[]);

	public:
		

		inline int getVectorsAmount() override
		{
			return (int)vertex.size();
		}

	};

}


#endif // !MODEL_3D_HEADER
