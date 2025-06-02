import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Character, CharacterStats, CharacterSkill, InventoryItem, StatUpdateEvent, SkillUpdateEvent } from '../../../models/character.model';
import { CharacterService } from '../../../services/character.service';

@Component({
  selector: 'app-character-screen',
  templateUrl: './character-screen.component.html',
  styleUrls: ['./character-screen.component.scss']
})
export class CharacterScreenComponent implements OnInit {
  characters: Character[] = [];
  selectedCharacter: Character | null = null;
  activePanel: string = 'overview';
  isLoading = false;
  showCharacterCreator = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private characterService: CharacterService
  ) {}

  ngOnInit(): void {
    this.loadCharacters();
    
    // Check for character ID in route
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.selectCharacterById(params['id']);
      }
    });
  }

  loadCharacters(): void {
    this.isLoading = true;
    // this.characterService.getCharacters().subscribe({
    //   next: (characters) => {
    //     this.characters = characters;
    //     this.isLoading = false;
        
    //     // Auto-select first character if none selected
    //     if (!this.selectedCharacter && characters.length > 0) {
    //       this.selectCharacter(characters[0]);
    //     }
    //   },
    //   error: (error) => {
    //     console.error('Error loading characters:', error);
    //     this.isLoading = false;
    //   }
    // });
  }

  selectCharacter(character: Character): void {
    this.selectedCharacter = character;
    this.router.navigate(['/character', character.id]);
  }

  selectCharacterById(id: string): void {
    const character = this.characters.find(c => c.id === id);
    if (character) {
      this.selectedCharacter = character;
    }
  }

  setActivePanel(panel: string): void {
    this.activePanel = panel;
  }

  onStatUpdate(event: StatUpdateEvent): void {
    if (!this.selectedCharacter) return;

    // this.characterService.updateCharacterStats(this.selectedCharacter.id, {
    //   ...this.selectedCharacter.stats,
    //   [event.statName]: event.value
    // }).subscribe({
    //   next: (updatedCharacter) => {
    //     this.selectedCharacter = updatedCharacter;
    //     this.updateCharacterInList(updatedCharacter);
    //   },
    //   error: (error) => {
    //     console.error('Error updating stats:', error);
    //   }
    // });
  }

  onSkillUpdate(event: SkillUpdateEvent): void {
    if (!this.selectedCharacter) return;

    // this.characterService.updateSkill(this.selectedCharacter.id, event.skillId, event.level).subscribe({
    //   next: (updatedCharacter) => {
    //     this.selectedCharacter = updatedCharacter;
    //     this.updateCharacterInList(updatedCharacter);
    //   },
    //   error: (error) => {
    //     console.error('Error updating skill:', error);
    //   }
    // });
  }

  onInventoryUpdate(inventory: InventoryItem[]): void {
    if (!this.selectedCharacter) return;

    // this.characterService.updateInventory(this.selectedCharacter.id, inventory).subscribe({
    //   next: (updatedCharacter) => {
    //     this.selectedCharacter = updatedCharacter;
    //     this.updateCharacterInList(updatedCharacter);
    //   },
    //   error: (error) => {
    //     console.error('Error updating inventory:', error);
    //   }
    // });
  }

  onCharacterCreated(character: Character): void {
    this.characters.push(character);
    this.selectedCharacter = character;
    this.showCharacterCreator = false;
    this.router.navigate(['/character', character.id]);
  }

  onCreatorCancelled(): void {
    this.showCharacterCreator = false;
  }

  createNewCharacter(): void {
    this.showCharacterCreator = true;
  }

  deleteCharacter(character: Character): void {
    if (confirm(`Tem certeza que deseja excluir o personagem "${character.name}"?`)) {
      this.characterService.deleteCharacter(character.id).subscribe({
        next: () => {
          this.characters = this.characters.filter(c => c.id !== character.id);
          if (this.selectedCharacter?.id === character.id) {
            this.selectedCharacter = this.characters.length > 0 ? this.characters[0] : null;
            if (this.selectedCharacter) {
              this.router.navigate(['/character', this.selectedCharacter.id]);
            } else {
              this.router.navigate(['/character']);
            }
          }
        },
        error: (error) => {
          console.error('Error deleting character:', error);
        }
      });
    }
  }

  private updateCharacterInList(updatedCharacter: Character): void {
    const index = this.characters.findIndex(c => c.id === updatedCharacter.id);
    if (index !== -1) {
      this.characters[index] = updatedCharacter;
    }
  }

  // getCharacterHealthPercentage(character: Character): number {
  //   return (character.currentHealth / character.maxHealth) * 100;
  // }

  // getCharacterManaPercentage(character: Character): number {
  //   return (character.currentMana / character.maxMana) * 100;
  // }

  getCharacterExperiencePercentage(character: Character): number {
    const expForCurrentLevel = this.getExperienceForLevel(character.level);
    const expForNextLevel = this.getExperienceForLevel(character.level + 1);
    const currentLevelExp = character.experience - expForCurrentLevel;
    const expNeededForNext = expForNextLevel - expForCurrentLevel;
    return (currentLevelExp / expNeededForNext) * 100;
  }

  private getExperienceForLevel(level: number): number {
    return Math.floor(100 * Math.pow(level, 1.5));
  }
}