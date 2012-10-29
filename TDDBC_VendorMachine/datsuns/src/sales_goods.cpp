#include <sales_goods.h>
#include <string>
#include <money.h>

//=====================================
//
//-------------------------------------
SalesGoods::SalesGoods()
{
}


//=====================================
//
//-------------------------------------
SalesGoods::SalesGoods( const std::string name, Money price )
{
    this->name = name;
    this->price = price;
}


//=====================================
//
//-------------------------------------
SalesGoods::~SalesGoods()
{
}


//=====================================
//
//-------------------------------------
const std::string & SalesGoods::get_name( void )
{
    return this->name;
}


//=====================================
//
//-------------------------------------
const Money & SalesGoods::get_price( void )
{
    return this->money;
}
