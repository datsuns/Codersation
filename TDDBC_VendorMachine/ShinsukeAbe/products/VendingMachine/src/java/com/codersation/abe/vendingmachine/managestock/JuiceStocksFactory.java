package com.codersation.abe.vendingmachine.managestock;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class JuiceStocksFactory {

    public static JuiceStocks createNewStocks() {
        JuiceStocks stocks = new JuiceStocks();
        
        stocks.add(new Juice("コーラ", 120), 5);
        
        return stocks;
    }
    
}
