package net.codersation.vendingmachine.stockflow;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

import net.codersation.vendingmachine.Juice;

public class JuiceStock implements Iterable<JuiceRack> {

	private List<JuiceRack> racks = new ArrayList<JuiceRack>();

	public JuiceStock() {
		initialize();
	}

	@Override
	public Iterator<JuiceRack> iterator() {
		return racks.iterator();
	}

	public List<JuiceRack> getRacks() {
		return Collections.unmodifiableList(racks);
	}

	private void initialize() {
		for (Juice juice : Juice.values()) {
			racks.add(new JuiceRack(juice, 5));
		}
	}
}
