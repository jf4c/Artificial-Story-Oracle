export interface Character {
  id: string;
  name: string;
  class: string;
  level: number;
  experience: number;
  avatar?: string;
  backstory?: string;
  stats: CharacterStats;
  skills: CharacterSkill[];
  inventory: InventoryItem[];
  health: {
    current: number;
    max: number;
  };
  mana: {
    current: number;
    max: number;
  };
  createdAt: Date;
  updatedAt: Date;
}

export interface CharacterStats {
  strength: number;
  dexterity: number;
  intelligence: number;
  charisma: number;
  constitution: number;
  wisdom: number;
}

export interface CharacterSkill {
  id: string;
  name: string;
  level: number;
  maxLevel: number;
  description: string;
  category: 'combat' | 'magic' | 'social' | 'utility';
}

export interface InventoryItem {
  id: string;
  name: string;
  type: 'weapon' | 'armor' | 'consumable' | 'misc';
  quantity: number;
  description: string;
  rarity: 'common' | 'uncommon' | 'rare' | 'epic' | 'legendary';
  equipped?: boolean;
  value?: number;
}

export interface StatUpdateEvent {
  statName: keyof CharacterStats;
  value: number;
}

export interface SkillUpdateEvent {
  skillId: string;
  level: number;
}