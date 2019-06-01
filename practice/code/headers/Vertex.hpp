#ifndef VERTEX_HEADER
#define VERTEX_HEADER
#include <vector>
namespace tool {
	class Vertex {
	public:
		std::vector<float> values;
	protected:
		Vertex(){}
	public:
		const float * addValue(float _value) { 
			values.push_back(_value);
			std::vector<float>::iterator it = values.end();
			return &(*it);
		}
	};
}

#endif // !VERTEX_HEADER
