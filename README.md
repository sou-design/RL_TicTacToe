# 🎮 Tic Tac Toe avec le reinforcement Learning (RL)

Le projet consiste en l'implémentation du jeu du **Morpion (Tic Tac Toe)** en utilisant le langage **C#** et la bibliothèque **Raylib** pour la gestion de l'interface graphique.  
L'objectif principal est d'introduire des joueurs artificiels utilisant des techniques d'apprentissage par renforcement pour améliorer leurs performances au fil du temps.
<img width="435" height="455" alt="image" src="https://github.com/user-attachments/assets/83e86623-711f-415a-abb9-28ff841aa293" />

---

## 🤖 Avantages de la méthode du reinforcement learning

L'utilisation de l'apprentissage par renforcement permet aux joueurs artificiels d'ajuster leurs stratégies en fonction des expériences passées.

---

## 🧠 Phases du jeu

### ● 🔄 Entrainement :

Pendant l'entraînement, chaque joueur doit rechercher les positions disponibles, choisir une action, mettre à jour l'état du plateau et ajouter l'action aux états du joueur, juger s'il atteint la fin du jeu et attribuer la récompense en conséquence.

### ● 💾 Sauvegarde et Chargement de la Politique :

À la fin de l'entraînement, la politique apprise par l'agent est sauvegardée dans le dictionnaire état-valeur. Cette politique est ensuite chargée pour jouer contre un joueur humain.

### ● 👤💻 Humain contre Machine :

La dernière étape consiste à permettre à un joueur humain de jouer contre l'agent.

---

## 🏗️ Implémentation

### ● Une classe State est nécessaire pour le jeu

Elle enregistre l'état du plateau pour les deux joueurs, met à jour l'état lorsque l'un des joueurs effectue une action, et peut juger de la fin du jeu et attribuer des récompenses en conséquence.

### ● Les classes Human et Machine

Représentant respectivement le joueur humain et les joueurs artificiels, ces classes implémentent les actions possibles pour chaque type de joueur. Les joueurs artificiels utilisent l'apprentissage par renforcement pour améliorer leurs stratégies au fil du temps.

### ● La classe GameUtility

Cette classe contient des utilitaires pour la génération de tous les états possibles du jeu, nécessaire pour l'apprentissage par renforcement.

### ● La classe RaylibManager

Cette classe gère l'interface graphique du jeu en utilisant la bibliothèque Raylib. Elle assure l'initialisation de la fenêtre, le dessin du plateau de jeu, et la capture des actions de la souris.
