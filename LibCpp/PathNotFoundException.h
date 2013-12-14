#pragma once
#include<exception>

using namespace std;

class PathNotFoundException : public exception
{
public:
	PathNotFoundException(void);
	~PathNotFoundException(void);

	virtual const char * what() const throw()
    {
        return "Path Not Found !";
    }
 
private:
};

