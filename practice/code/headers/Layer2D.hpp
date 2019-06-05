#ifndef LAYER_2D_HEADER
#define LAYER_2D_HEADER

#include "Layer.hpp"

namespace tool
{
	class Layer2D : public Layer
	{
	private:
		std::vector<std::shared_ptr<Model2D>> models;

	public:
		Layer2D(std::string name_) : Layer(name_){}

	public:
		inline const int getModels2dAmount()
		{
			return (int)models.size();
		}

		inline const int getVectorsAmount(int model)
		{
			return models[model]->getVectorsAmount();
		}

		inline const std::string getModel2dName(int model)
		{
			return models[model]->getName();
		}

		void Layer::getColor(int model, Color* color)
		{
			models[model]->modifyColor(color);
		}

		void addModel(std::string name, std::shared_ptr<Model2D> model);

		void fillVectors(int model, Vector2 vectors[]);

		inline const std::shared_ptr<Model2D> getModel2d(int model) 
		{
			return models[model];
		}

	};
}

#endif // !LAYER_2D_HEADER
