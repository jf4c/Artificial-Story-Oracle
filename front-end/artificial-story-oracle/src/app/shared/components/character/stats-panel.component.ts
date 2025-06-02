import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CharacterStats, StatUpdateEvent } from '../../../models/character.model';

@Component({
  selector: 'app-stats-panel',
  templateUrl: './stats-panel.component.html',
  styleUrls: ['./stats-panel.component.scss']
})
export class StatsPanelComponent {
  @Input() stats!: CharacterStats;
  @Output() statUpdate = new EventEmitter<StatUpdateEvent>();

  availablePoints = 0;

  statDescriptions = {
    strength: 'Determina o dano físico e capacidade de carregar itens',
    dexterity: 'Afeta velocidade, precisão e esquiva',
    intelligence: 'Influencia poder mágico e pontos de mana',
    charisma: 'Melhora interações sociais e liderança',
    constitution: 'Aumenta pontos de vida e resistência',
    wisdom: 'Afeta percepção e resistência mental'
  };

  statNames = {
    strength: 'Força',
    dexterity: 'Destreza',
    intelligence: 'Inteligência',
    charisma: 'Carisma',
    constitution: 'Constituição',
    wisdom: 'Sabedoria'
  };

  getStatEntries(): Array<{key: keyof CharacterStats, value: number}> {
    return Object.entries(this.stats).map(([key, value]) => ({
      key: key as keyof CharacterStats,
      value: value as number
    }));
  }

  handleStatChange(statName: keyof CharacterStats, change: number): void {
    const currentValue = this.stats[statName];
    const newValue = Math.max(1, Math.min(20, currentValue + change));
    
    if (change > 0 && this.availablePoints <= 0) return;
    if (newValue === currentValue) return;

    this.statUpdate.emit({ statName, value: newValue });
    this.availablePoints -= change;
  }

  getStatModifier(value: number): string {
    const modifier = Math.floor((value - 10) / 2);
    return modifier >= 0 ? `+${modifier}` : `${modifier}`;
  }

  getStatColor(value: number): string {
    if (value >= 16) return '#4ecdc4';
    if (value >= 13) return '#ffd93d';
    if (value >= 10) return '#ffffff';
    return '#ff6b6b';
  }

  getStatPercentage(value: number): number {
    return (value / 20) * 100;
  }
}