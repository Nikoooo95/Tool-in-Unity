#ifndef LAYER_3D_HEADER
#define LAYER_3D_HEADER
#include "Layer.hpp"
namespace tool
{
	class Layer3D : public Layer
	{
	private:
		std::vector<std::shared_ptr<Model3D>> models;

	public:
		Layer3D(std::string name_) : Layer(name_){}

	public:
		inline const int getModels3dAmount()
		{
			return (int)models.size();
		}

		inline const int getVectors3dAmount(int model)
		{
			return models[model]->getVectorsAmount();
		}

		inline const std::string getModel3dName(int model)
		{
			return models[model]->getName();
		}

		void Layer::getColor(int model, Color* color)
		{
			models[model]->modifyColor(color);
		}

		void addModel(std::string name, std::shared_ptr<Model3D> model);
		void fillVectors(int model, Vector3 vectors[]);
	};
}

#endif // !LAYER_3D_HEADER
