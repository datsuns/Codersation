package net.codersation.vendingmachine;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.moneyflow.MoneyFlow;
import net.codersation.vendingmachine.stockflow.JuiceRack;
import net.codersation.vendingmachine.stockflow.JuiceStock;

public class VendingMachine {

	public MoneyFlow money = new MoneyFlow();
	private List<Money> credit = new ArrayList<>();
	
	private List<Money> change = new ArrayList<>();
	private MoneyPolicy moneyPoricy = new MoneyPolicy();
	private JuiceStock juiceStock = new JuiceStock();
	public int getTotalAmount() {
		int totalAmount = 0;
		for (Money c : credit) {
			totalAmount += c.getValue();
		}
		return totalAmount;
	}

	public void insert(Money money) {
		if (moneyPoricy.isAllowed(money)) {
			credit.add(money);
		} else {
			change.add(money);
		}
	}

	public void payBack() {
		change.addAll(credit);
		credit.clear();
	}

	public int getChangeAmount() {
		int totalAmount = 0 ;
		for (Money c : change) {
			totalAmount += c.getValue();
		}
		return totalAmount;
	}

	public void purchase(Juice juice) {
		JuiceRack stock = juiceStock.getRack(juice);
		if (stock.canPurchase(getTotalAmount())) {
			stock.remove();

			money.addSale(juice.getPrice());
			
			List<Money> useMoneyList = CreditService.getUseMoneyList(credit, juice.getPrice());
			int tempAmount = 0;
			for (Money money : useMoneyList) {
				credit.remove(money);
				tempAmount += money.getValue();
			}
			if (tempAmount != juice.getPrice()) {
				List<Money> list = new ArrayList<>(Collections.nCopies(100, Money.TenYen));
				List<Money> l = CreditService.getUseMoneyList(list, tempAmount - juice.getPrice());
				credit.addAll(l);
			}
		}
	}

	public int getSaleAmount() {
		return money.getSaleAmount();
	}

	public List<Juice> getPurchasable() {
		List<Juice> list = new ArrayList<>();
		for (Juice juice : Juice.values()) {
			if (juiceStock.getRack(juice).canPurchase(getTotalAmount())) {
				list.add(juice);
			}
		}
		return list;
	}

	public List<JuiceRack> getAllJuiceStock() {
		return juiceStock.getRacks();
	}
}
