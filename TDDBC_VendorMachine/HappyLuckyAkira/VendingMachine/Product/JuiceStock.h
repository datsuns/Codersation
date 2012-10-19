/*
 * JuiceStock.h
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#ifndef JUICESTOCK_H_
#define JUICESTOCK_H_

class Juice;
class JuiceRack;
#include <list>
#include <boost/shared_ptr.hpp>

class JuiceStock
{
public:
  explicit JuiceStock(const Juice& juice, int count);
  virtual
  ~JuiceStock();
    bool IsInStock(const Juice& juice );
    bool AddJuice(const Juice& juice ,int count);
private:
    JuiceStock();
    typedef boost::shared_ptr<JuiceRack> JuiceRackPointer;
    std::list<JuiceRackPointer> juicerack_list_;
};

class IsInRack : public std::binary_function<boost::shared_ptr<JuiceRack>,Juice,bool> {
public:
  bool operator()(boost::shared_ptr<JuiceRack> pjuicerack, const Juice& juice) const;
 };

#endif /* JUICESTOCK_H_ */
