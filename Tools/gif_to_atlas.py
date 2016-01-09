from PIL import Image
import sys

source_path = '/Users/jeremie/Desktop/Player.gif'
result_path = '/Users/jeremie/Desktop/Player.png'

gif_image = Image.open(source_path)
width, height = gif_image.size
number_of_frames = 145
number_of_columns = 10

result_image = Image.new('RGBA', (width * number_of_columns, height * (1 + number_of_frames / number_of_columns)))
frame_index = 0
print('Generating png atlas of size (%d, %d).' % (width * number_of_columns, height * (1 + number_of_frames / number_of_columns)))
while gif_image:
	sys.stdout.write('.')
	result_image.paste(gif_image, ((frame_index % number_of_columns) * width, (frame_index / number_of_columns) * height))
	frame_index += 1
	try:
		gif_image.seek(frame_index)
	except EOFError:
		break
sys.stdout.write('\n')
print('Atlas %s generated successfuly!' % result_path)
result_image.save(result_path)
