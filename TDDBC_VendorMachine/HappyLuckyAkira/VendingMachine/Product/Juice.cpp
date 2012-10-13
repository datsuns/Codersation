/*
 * Juice.cpp
 *
 *  Created on: 2012/10/07
 *      Author: Akira
 */

#include "Juice.h"

//Juiceのコンストラクタ
Juice::Juice(const std::string& name, int price)
:name_(name),price_(price)
{
}

//C++11なら、deleteしておくところ
Juice::Juice()
{

}

Juice::~Juice()
{
}

