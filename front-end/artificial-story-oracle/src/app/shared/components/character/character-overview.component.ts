import { Component, Input } from '@angular/core';
import { Character } from '../../../models/character.model';

@Component({
  selector: 'app-character-overview',
  templateUrl: './character-overview.component.html',
  styleUrls: ['./character-overview.component.scss']
})
export class CharacterOverviewComponent {
  @Input() character!: Character;

  getStatModifier(value: number): string {
    const modifier = Math.floor((value - 10) / 2);
    return modifier >= 0 ? `+${modifier}` : `${modifier}`;
  }

  get totalSkillPoints(): number {
    return this.character.skills.reduce((total, skill) => total + skill.level, 0);
  }

  get averageSkillLevel(): number {
    const activeSkills = this.character.skills.filter(skill => skill.level > 0);
    if (activeSkills.length === 0) return 0;
    return activeSkills.reduce((total, skill) => total + skill.level, 0) / activeSkills.length;
  }
}