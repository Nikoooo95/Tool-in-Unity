#ifndef LAYER_HEADER
#define LAYER_HEADER
#include <map>
#include <string>
#include "Model2D.hpp"
namespace tool {
	class Layer {
	private:
		std::string name;
		std::map<std::string, std::shared_ptr<Model2D>> models;
	public:
		Layer(std::string _name) : name(_name){}

	public:
		void addModel(std::string name, std::shared_ptr<Model2D> model);
	};
}

#endif // !LAYER_HEADER
