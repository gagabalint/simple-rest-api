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
    function displayResults(formations) {
        resultsSection.innerHTML = '<h3>Generált felállások</h3>';
        
        formations.sort((a, b) => b.rating - a.rating);
        
        formations.forEach((formation, index) => {
            let colorClass;
            if (index === 0) colorClass = 'bg-success'; 
            else if (index === formations.length - 1) colorClass = 'bg-danger'; 
            else colorClass = 'bg-warning';
            
            const formationCard = document.createElement('div');
            formationCard.className = `card mb-4 ${colorClass}`;
            
            let cardContent = `
                <div class="card-header">
                    <h4>Felállás: ${formation.formation} - Értékelés: ${formation.rating}%</h4>
                </div>
                <div class="card-body">
                    <div class="row">`;
            
            const positions = {
                'GK': [], 'DF': [], 'MF': [], 'FW': []
            };
            
            formation.players.forEach(player => {
                positions[player.assignedPosition].push(player.player.name);
            });
            
            cardContent += `
                <div class="col-12 mb-3 text-center">
                    <strong>Kapus:</strong> ${positions['GK'].join(', ')}
                </div>
                <div class="col-12 mb-3 text-center">
                    <strong>Védők:</strong> ${positions['DF'].join(', ')}
                </div>
                <div class="col-12 mb-3 text-center">
                    <strong>Középpályások:</strong> ${positions['MF'].join(', ')}
                </div>
                <div class="col-12 mb-3 text-center">
                    <strong>Támadók:</strong> ${positions['FW'].join(', ')}
                </div>`;
            
            cardContent += `
                    </div>
                </div>`;
            
            formationCard.innerHTML = cardContent;
            resultsSection.appendChild(formationCard);
        });
    }
});