import { Component, Input } from '@angular/core';
import { Character } from '../../../models/character.model';

@Component({
  selector: 'app-character-card',
  templateUrl: './character-card.component.html',
  styleUrls: ['./character-card.component.scss']
})
export class CharacterCardComponent {
  @Input() character!: Character;

  get healthPercentage(): number {
    return (this.character.health.current / this.character.health.max) * 100;
  }

  get manaPercentage(): number {
    return (this.character.mana.current / this.character.mana.max) * 100;
  }

  get experienceToNext(): number {
    return (this.character.level * 1000) - this.character.experience;
  }

  get experiencePercentage(): number {
    return ((this.character.experience % 1000) / 1000) * 100;
  }

  getAvatarInitial(): string {
    return this.character.name.charAt(0).toUpperCase();
  }
}