import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CharacterSkill, SkillUpdateEvent } from '../../../models/character.model';

@Component({
  selector: 'app-skills-panel',
  templateUrl: './skills-panel.component.html',
  styleUrls: ['./skills-panel.component.scss']
})
export class SkillsPanelComponent {
  @Input() skills: CharacterSkill[] = [];
  @Output() skillUpdate = new EventEmitter<SkillUpdateEvent>();

  selectedCategory: string = 'all';
  availableSkillPoints = 5;

  categories = [
    { id: 'all', name: 'Todas', icon: '📋' },
    { id: 'combat', name: 'Combate', icon: '⚔️' },
    { id: 'magic', name: 'Magia', icon: '🔮' },
    { id: 'social', name: 'Social', icon: '💬' },
    { id: 'utility', name: 'Utilidade', icon: '🔧' }
  ];

  get filteredSkills(): CharacterSkill[] {
    return this.selectedCategory === 'all' 
      ? this.skills 
      : this.skills.filter(skill => skill.category === this.selectedCategory);
  }

  onCategoryChange(categoryId: string): void {
    this.selectedCategory = categoryId;
  }

  handleSkillLevelChange(skillId: string, change: number): void {
    const skill = this.skills.find(s => s.id === skillId);
    if (!skill) return;

    const newLevel = Math.max(0, Math.min(skill.maxLevel, skill.level + change));
    
    if (change > 0 && this.availableSkillPoints <= 0) return;
    if (newLevel === skill.level) return;

    this.skillUpdate.emit({ skillId, level: newLevel });
    this.availableSkillPoints -= change;
  }

  getSkillColor(level: number, maxLevel: number): string {
    const percentage = level / maxLevel;
    if (percentage >= 0.8) return '#4ecdc4';
    if (percentage >= 0.6) return '#ffd93d';
    if (percentage >= 0.4) return '#ffb74d';
    if (percentage >= 0.2) return '#ff8e8e';
    return '#666';
  }

  getSkillPercentage(level: number, maxLevel: number): number {
    return (level / maxLevel) * 100;
  }

  getCategoryIcon(category: string): string {
    const categoryData = this.categories.find(c => c.id === category);
    return categoryData ? categoryData.icon : '📋';
  }
}