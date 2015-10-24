
function draw_board(board) {
  cell_size = 40
  lines = board.split('\n')
  board_length = lines.length
  if (lines[lines.length - 1].length == 0)
	board_length -= 1
  blackCount = 0
  whiteCount = 0
  var canvas = document.getElementById("boardCanvas");
  var ctx = canvas.getContext("2d");
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  for (var i = 0; i <= board_length; i++) {
	ctx.moveTo(cell_size*i, 0);
	ctx.lineTo(cell_size*i, cell_size*board_length);
	ctx.stroke()
	ctx.moveTo(0, cell_size*i);
	ctx.lineTo(cell_size*board_length, cell_size*i);
	ctx.stroke()
  }
  for (var i = 0; i < board_length; i++) {
  	for (var j = 0; j < board_length; j++) {
  	  if(lines[j][i] == '-')
    		continue
  	  ctx.beginPath();
  	  ctx.arc((i+0.5)*cell_size ,(j+0.5)*cell_size,cell_size/2,0,2*Math.PI);
  	  if(lines[j][i] == 'O') {
        ctx.fillStyle = 'black'
    		blackCount++;
  	  }
  	  else {
        ctx.fillStyle = 'white'
  		  whiteCount++;
  	  }
      ctx.fill()
      ctx.stroke()
  	}
  }
  $("#whiteCount").text(whiteCount)
  $("#blackCount").text(blackCount)
}

delay_ms = 1000

function play_game(data) {
  data_list = data.split("#\r\n")
  playerNames = data_list[0].split(",")
  $("#whitePlayer").text(playerNames[0])
  $("#blackPlayer").text(playerNames[1])
  boards = data_list[1].split('$\r\n')
  load_boards(boards, 0)
}

function load_boards(boards, i) {
  if (i == boards.length - 1)
	return
  draw_board(boards[i]);
  setTimeout(function() {load_boards(boards, i+1)},1000)
}

$(document).ready(function(){
  $.get("dump.txt", function(data) {
	play_game(data)
  })
});
