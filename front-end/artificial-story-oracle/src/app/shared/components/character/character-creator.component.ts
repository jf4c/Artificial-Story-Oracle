import { Component, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Character, CharacterStats } from '../../../models/character.model';
import { CharacterService } from '../../../services/character.service';

@Component({
  selector: 'app-character-creator',
  templateUrl: './character-creator.component.html',
  styleUrls: ['./character-creator.component.scss']
})
export class CharacterCreatorComponent {
  @Output() characterCreated = new EventEmitter<Character>();
  @Output() cancelled = new EventEmitter<void>();

  characterForm: FormGroup;
  currentStep = 1;
  totalSteps = 4;
  
  availableStatPoints = 27;
  stats: CharacterStats = {
    strength: 8,
    dexterity: 8,
    intelligence: 8,
    charisma: 8,
    constitution: 8,
    wisdom: 8
  };

  characterClasses = [
    {
      id: 'warrior',
      name: 'Guerreiro',
      description: 'Especialista em combate corpo a corpo com alta resistência',
      icon: '⚔️',
      statBonuses: { strength: 2, constitution: 1 },
      startingSkills: ['swordplay', 'intimidation']
    },
    {
      id: 'mage',
      name: 'Mago',
      description: 'Manipulador de energias arcanas com vasto conhecimento',
      icon: '🔮',
      statBonuses: { intelligence: 2, wisdom: 1 },
      startingSkills: ['fire-magic', 'healing']
    },
    {
      id: 'rogue',
      name: 'Ladino',
      description: 'Especialista em furtividade e habilidades técnicas',
      icon: '🗡️',
      statBonuses: { dexterity: 2, charisma: 1 },
      startingSkills: ['stealth', 'lockpicking']
    },
    {
      id: 'ranger',
      name: 'Batedor',
      description: 'Explorador hábil com arcos e conhecimento da natureza',
      icon: '🏹',
      statBonuses: { dexterity: 1, wisdom: 2 },
      startingSkills: ['archery', 'stealth']
    }
  ];

  selectedClass: any = null;

  constructor(
    private fb: FormBuilder,
    private characterService: CharacterService
  ) {
    this.characterForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      backstory: ['']
    });
  }

  get statNames() {
    return {
      strength: 'Força',
      dexterity: 'Destreza',
      intelligence: 'Inteligência',
      charisma: 'Carisma',
      constitution: 'Constituição',
      wisdom: 'Sabedoria'
    };
  }

  get totalAllocatedPoints(): number {
    return Object.values(this.stats).reduce((total, value) => total + value, 0) - 48; // 48 = 8 * 6 stats
  }

  get remainingPoints(): number {
    return this.availableStatPoints - this.totalAllocatedPoints;
  }

  nextStep(): void {
    if (this.currentStep < this.totalSteps) {
      this.currentStep++;
    }
  }

  previousStep(): void {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
  }

  selectClass(characterClass: any): void {
    this.selectedClass = characterClass;
  }

  adjustStat(statName: keyof CharacterStats, change: number): void {
    const currentValue = this.stats[statName];
    const newValue = currentValue + change;
    
    // Verificar limites
    if (newValue < 8 || newValue > 15) return;
    
    // Verificar pontos disponíveis
    if (change > 0 && this.remainingPoints <= 0) return;
    
    this.stats[statName] = newValue;
  }

  getStatCost(value: number): number {
    if (value <= 13) return value - 8;
    if (value === 14) return 7;
    if (value === 15) return 9;
    return 0;
  }

  getStatModifier(value: number): string {
    const modifier = Math.floor((value - 10) / 2);
    return modifier >= 0 ? `+${modifier}` : `${modifier}`;
  }

  rollRandomStats(): void {
    const points = 27;
    const baseStats = { ...this.stats };
    
    // Reset to base values
    Object.keys(baseStats).forEach(key => {
      baseStats[key as keyof CharacterStats] = 8;
    });
    
    let remainingPoints = points;
    const statKeys = Object.keys(baseStats) as (keyof CharacterStats)[];
    
    // Distribute points randomly
    while (remainingPoints > 0) {
      const randomStat = statKeys[Math.floor(Math.random() * statKeys.length)];
      if (baseStats[randomStat] < 15) {
        const cost = this.getStatCost(baseStats[randomStat] + 1);
        if (cost <= remainingPoints) {
          baseStats[randomStat]++;
          remainingPoints -= cost;
        }
      }
    }
    
    this.stats = baseStats;
  }

  canProceed(): boolean {
    switch (this.currentStep) {
      case 1:
        return this.characterForm.get('name')?.valid || false;
      case 2:
        return this.selectedClass !== null;
      case 3:
        return this.remainingPoints === 0;
      case 4:
        return true;
      default:
        return false;
    }
  }

  createCharacter(): void {
    if (!this.characterForm.valid || !this.selectedClass) return;

    const formValue = this.characterForm.value;
    
    // Apply class bonuses to stats
    const finalStats = { ...this.stats };
    if (this.selectedClass.statBonuses) {
      Object.entries(this.selectedClass.statBonuses).forEach(([stat, bonus]) => {
        finalStats[stat as keyof CharacterStats] += bonus as number;
      });
    }

    const newCharacter = this.characterService.createCharacter(
      formValue.name,
      this.selectedClass.id,
      finalStats,
      formValue.backstory
    );

    // Apply starting skills
    if (this.selectedClass.startingSkills) {
      this.selectedClass.startingSkills.forEach((skillId: string) => {
        this.characterService.updateSkill(skillId, 1);
      });
    }

    this.characterCreated.emit(newCharacter);
  }

  cancel(): void {
    this.cancelled.emit();
  }
}