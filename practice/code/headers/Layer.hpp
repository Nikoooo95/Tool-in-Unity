#ifndef LAYER_HEADER
#define LAYER_HEADER

#include <map>
#include <string>

#include "Model2D.hpp"

namespace tool 
{
	class Layer 
	{

	private:
		std::string name;
		std::vector<std::shared_ptr<Model2D>> models;

	public:
		Layer(std::string _name) : name(_name){}

	public:
		void addModel(std::string name, std::shared_ptr<Model2D> model);
		void fillVectors(int model, Vector2 vectors[]);

	public:

		inline const std::string getName()
		{
			return name;
		}

		inline const int getModelsAmount() 
		{
			return (int)models.size();
		}

		inline const int getVectorsAmount(int model) 
		{
			return models[model]->getVectorsAmount();
		}

		inline const std::string getModelName(int model) 
		{
			return models[model]->getName();
		}

		
		
	};
}

#endif // !LAYER_HEADER
