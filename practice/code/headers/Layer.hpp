#ifndef LAYER_HEADER
#define LAYER_HEADER
#include <map>
#include <string>
#include "Model2D.hpp"
namespace tool {
	class Layer {
	private:
		std::string name;
		std::vector<std::shared_ptr<Model2D>> models;
	public:
		Layer(std::string _name) : name(_name){}

	public:
		void addModel(std::string name, std::shared_ptr<Model2D> model);

		void getVertexFromModels();

		const std::string getName(){
			return name;
		}

		const int getModelsAmount() {
			return models.size();
		}

		const int getVectorsAmount(int model) {
			return models[model]->getVectorsAmount();
		}

		const std::string getModelName(int model) {
			return models[model]->getName();
		}

		void fillVectors(int model, Vector2 vectors[]) {
			models[model]->fillVectors(vectors);
		}
	};
}

#endif // !LAYER_HEADER
