# EXTERNAL startQuiz(quizName)
#        ~ startQuiz("test")
EXTERNAL startQuiz(cobaString)
EXTERNAL giveSpeakerName(charName)
#EXTERNAL chosen(Jawaban)
~ giveSpeakerName("Guru")

-> main


=== main ===
Apakah kamu ingin masuk ke sekolah dan memulai belajar ?
#CLEAR
    + [YA]
        -> chosen("Selamat Belajar")
        #Selamat Belajar

    + [TIDAK]
        Jangan Dulu
        -> DONE    
        #-> chosen("Jangan Dulu")
    
=== chosen(Jawaban) ===
{Jawaban}
~ startQuiz("cobaString")

-> END