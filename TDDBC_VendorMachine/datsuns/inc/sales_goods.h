#ifndef SALES_GOODS_H_INICLUDED
#define SALES_GOODS_H_INICLUDED

#include <string>
#include <money.h>

class SalesGoods{
  public:
    SalesGoods();
    SalesGoods( const std::string name, Money price );
    ~SalesGoods();

    const std::string & get_name();
    const std::string & get_price();

  private:
    std::string name;
    Money price;
};

#endif
