let players = [];

document.addEventListener('DOMContentLoaded', function() {
    const playerForm = document.getElementById('playerForm');
    const generateBtn = document.getElementById('generateBtn');
    const resultsSection = document.getElementById('results');

    playerForm.onsubmit = function() {
        const playerName = document.getElementById('playerName').value;
        const playerPosition = document.getElementById('playerPosition').value;
        
        if (playerName && playerPosition) {
            players.push({
                name: playerName,position: playerPosition,isAssigned: true});
            
            
            const playerList = document.getElementById('playerList');
            const listItem = document.createElement('li');
            listItem.className = 'list-group-item';
            listItem.textContent = `${playerName} - ${(playerPosition)}`;
            playerList.appendChild(listItem);
            
            document.getElementById('playerName').value = '';
            document.getElementById('playerPosition').value = '';
            
            generateBtn.disabled = players.length < 11;
        }
        
        
        return false;
    };
    generateBtn.onclick = function() {
        // API kérés a backendhez
        fetch('http://localhost:5209/Lineup', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(players)
        })
        .then(response => response.json())
        .then(data => {
            displayResults(data);
        })
     
    };
    generateBtn.disabled = true

});