#ifndef MONEY_H_INCLUDED
#define MONEY_H_INCLUDED

class Money{
  public:
    Money();
    Money( int val );
    ~Money();

    int get( void );

  private:
    int money;
};


#endif
