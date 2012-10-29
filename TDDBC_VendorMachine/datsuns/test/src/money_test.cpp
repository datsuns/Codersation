#include <gtest/gtest.h>
#include <money.h>
#include <iostream>

class MoneyTest : public ::testing::Test {
  public:
    virtual void SetUp() {
    }

    virtual void TearDown() {
    }
  protected:
    Money money;
};

TEST_F( MoneyTest, GetSpecifiedMoney ){
    Money m(120);
    ASSERT_EQ( m.get(), 120 );
}
