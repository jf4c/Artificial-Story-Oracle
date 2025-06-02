import { Component, Input, Output, EventEmitter } from '@angular/core';
import { InventoryItem } from '../../../models/character.model';

@Component({
  selector: 'app-inventory-panel',
  templateUrl: './inventory-panel.component.html',
  styleUrls: ['./inventory-panel.component.scss']
})
export class InventoryPanelComponent {
  @Input() inventory: InventoryItem[] = [];
  @Output() inventoryUpdate = new EventEmitter<InventoryItem[]>();

  selectedFilter: string = 'all';
  sortBy: 'name' | 'type' | 'rarity' | 'value' = 'name';
  sortDirection: 'asc' | 'desc' = 'asc';

  itemTypes = [
    { id: 'all', name: 'Todos', icon: '📦' },
    { id: 'weapon', name: 'Armas', icon: '⚔️' },
    { id: 'armor', name: 'Armaduras', icon: '🛡️' },
    { id: 'consumable', name: 'Consumíveis', icon: '🧪' },
    { id: 'misc', name: 'Diversos', icon: '📜' }
  ];

  rarityColors = {
    common: '#9e9e9e',
    uncommon: '#4caf50',
    rare: '#2196f3',
    epic: '#9c27b0',
    legendary: '#ff9800'
  };

  get filteredAndSortedInventory(): InventoryItem[] {
    let filtered = this.selectedFilter === 'all' 
      ? this.inventory 
      : this.inventory.filter(item => item.type === this.selectedFilter);

    return filtered.sort((a, b) => {
      let comparison = 0;
      
      switch (this.sortBy) {
        case 'name':
          comparison = a.name.localeCompare(b.name);
          break;
        case 'type':
          comparison = a.type.localeCompare(b.type);
          break;
        case 'rarity':
          const rarityOrder = ['common', 'uncommon', 'rare', 'epic', 'legendary'];
          comparison = rarityOrder.indexOf(a.rarity) - rarityOrder.indexOf(b.rarity);
          break;
        case 'value':
          comparison = (a.value || 0) - (b.value || 0);
          break;
      }

      return this.sortDirection === 'asc' ? comparison : -comparison;
    });
  }

  get totalValue(): number {
    return this.inventory.reduce((total, item) => total + ((item.value || 0) * item.quantity), 0);
  }

  get totalWeight(): number {
    return this.inventory.reduce((total, item) => total + item.quantity, 0);
  }

  onFilterChange(filter: string): void {
    this.selectedFilter = filter;
  }

  onSortChange(sortBy: 'name' | 'type' | 'rarity' | 'value'): void {
    if (this.sortBy === sortBy) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = sortBy;
      this.sortDirection = 'asc';
    }
  }

  onEquipItem(item: InventoryItem): void {
    if (item.type === 'weapon' || item.type === 'armor') {
      const updatedInventory = this.inventory.map(invItem => {
        if (invItem.id === item.id) {
          return { ...invItem, equipped: !invItem.equipped };
        }
        // Desequipar outros itens do mesmo tipo
        if (invItem.type === item.type && invItem.equipped && invItem.id !== item.id) {
          return { ...invItem, equipped: false };
        }
        return invItem;
      });
      
      this.inventoryUpdate.emit(updatedInventory);
    }
  }

  onUseItem(item: InventoryItem): void {
    if (item.type === 'consumable' && item.quantity > 0) {
      const updatedInventory = this.inventory.map(invItem => {
        if (invItem.id === item.id) {
          const newQuantity = invItem.quantity - 1;
          return newQuantity > 0 ? { ...invItem, quantity: newQuantity } : null;
        }
        return invItem;
      }).filter(item => item !== null) as InventoryItem[];
      
      this.inventoryUpdate.emit(updatedInventory);
    }
  }

  onDropItem(item: InventoryItem): void {
    const updatedInventory = this.inventory.filter(invItem => invItem.id !== item.id);
    this.inventoryUpdate.emit(updatedInventory);
  }

  getRarityColor(rarity: string): string {
    return this.rarityColors[rarity as keyof typeof this.rarityColors] || '#9e9e9e';
  }

  getTypeIcon(type: string): string {
    const typeData = this.itemTypes.find(t => t.id === type);
    return typeData ? typeData.icon : '📦';
  }

  getRarityName(rarity: string): string {
    const rarityNames = {
      common: 'Comum',
      uncommon: 'Incomum',
      rare: 'Raro',
      epic: 'Épico',
      legendary: 'Lendário'
    };
    return rarityNames[rarity as keyof typeof rarityNames] || 'Comum';
  }
}