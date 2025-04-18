using System.Collections.Generic;

namespace CombarTracker
{
    public class NPC
    {
        private List<string> _physicalTraits = new List<string>() { "У этого NPC все лицо в ужасных прыщах",
        "Глаза этого NPC прищурены, словно он смотрит на яркий свет", "У NPC есть татуировка которую он показывает каждый раз, когда кого ни будь встречает",
        "У этого NPC роскошные, угольно-черные, длинные волосы, которые превосходят его собственный рост",
        "У этого NPC лицо, которое может полюбить только мать", "Телосложение этого NPC выглядит одновременно мускулистым и долговязым",
        "У этого NPC худое, изящное телосложение", "У этого NPC отсутствует один или несколько зубов", "У этого NPC большие густые брови",
        "У этого NPC есть отличительная военная татуировка", "У этого NPC пальцы немного длиннее",
        "Кожа на руках этого NPC выглядит так, будто восстанавливается после сильного химического ожога",
        "Кожа этого NPC покрыта боевыми шрамами", "Этот NPC — ампидекс", "У этого NPC торчащие зубы",
        "У этого NPC неопрятный и неряшливый вид", "У этого NPC невероятно холодные руки", "У этого NPC есть характерный акцент, который отличает его",
        "У этого NPC горб", "У этого NPC шипящий голос", "У этого NPC слезятся глаза", "Этот NPC говорит очень быстро",
        "У этого NPC есть характерная культовая татуировка", "У этого NPC высокий, пискливый голос", "У этого NPC большой шрам на подбородке",
        "У NPC есть механическая замена нескольким пальцам/кисти/руке", "У этого NPC великолепные глаза", "Этот NPC всегда носит только лучшую одежду",
        "У этого NPC есть участок обесцвеченной кожи вокруг старого шрама ", "У этого NPC всегда какие-то красные пятна на руках",
        "Этот NPC всегда пахнет сельскохозяйственными животными", "У этого NPC деревянная нога",
        "У этого NPC на каждой руке по Х пальцев вместо 5", "Этот NPC НЕВЕРОЯТНО красив. Люди часто дважды смотрят на него, когда проходят мимо",
        "У этого NPC глубокий баритон", "Этот NPC УЖАСНО пахнет", "У этого NPS прическа в виде улья",
        "Этот NPC одевается как неряха и имеет тенденцию неделями носить одну и ту же одежду",
        "У этого NPC отсутствует X пальцев из-за несчастного случая на ферме", "У этого NPC нет пальца в результате несчастного случая на кухне",
        "У этого NPC есть характерная татуировка раба", "У этого NPC аномально длинный нос", "Этот NPC хромает из-за несчастного случая",
        "Этот NPC носит рваную, грязную одежду", "У этого NPS монашеская стрижка", "Этот NPC никогда никому не смотрит в глаза",
        "У этого NPC очень выраженные скулы и впалые щеки", "У этого NPC много веснушек",
        "У этого NPC заметно кривые зубы, Он пытается скрыть свой рот во время разговора", "У этого NPC крошечный нос",
        "У этого NPC глубокий, резонирующий голос", "У этого NPC веснушки на лице, расположенные таким образом, что напоминают руны",
        "У этого NPC огромные выпуклые мышцы", "У этого NPC отсутствует язык, что делает его немым", "У этого NPC счастливые, теплые глаза",
        "Этот NPC лысый (лысый от природы, бритая голова)", "Этот NPC исключительно низкого роста для своего вида",
        "У этого персонажа NPS прическа «самурайский пучок»", "Этот NPC косоглаз", "У этого NPC аллергия, из-за которой он часто чихает",
        "NPC постоянно горбятся. Если бы они вытянулись вверх, то стали бы на целый фут выше", "У этого NPC густая, мохнатая монобровь",
        "У этого NPC на руке татуировка русалки", "Этот NPC подвержен несчастным случаям, постоянно спотыкается о булыжники, проливает напитки и еду на себя и других",
        "Этот NPC всегда пахнет ладаном", "У этого NPC зоркие глаза", "У этого NPC самые великолепные усы",
        "У этого NPC есть татуировка в виде якоря", "У этого NPC на лице шрамы от прыщей", "У этого NPC сонные глаза",
        "У этого NPC есть татуировка в виде черепа", "У этого NPC холодные, расчетливые глаза", "У этого NPC рот широко открыт",
        "У этого NPC есть характерная татуировка с изображением парусника",
        "У этого NPC большой шрам над правым глазом. Каждый раз, когда их об этом спрашивают, их история о том, как они его получили, меняется",
        "Этот NPC всегда кажется покрытым грязью из-за работы на (полях, шахтах, в грязи)", "У этого НПС дреды", "У этого NPC дикие глаза",
        "У этого NPC огромные уши", "Этот NPC всегда пахнет рыбой", "У этого NPC улыбка и смех, которые всегда кажутся по-настоящему гостеприимными",
        "У этого NPC явно много раз ломали нос", "У этого NPC искусно проколоты уши", "У этого NPC модная, стильная одежда",
        "У этого NPC сухие, потрескавшиеся губы",
        "Похоже, что у этого NPC есть как некоторые черты, общие с населением текущей страны, так и некоторые черты населения, находящегося за много-много миль от него", "Этот NPC носит слуховую трубку, чтобы слышать",
        "У этого NPC острые уши", "Этот NPC пухлый", "У этого NPC есть татуировка в виде кинжала", "У этого NPC гниющие зубы",
        "Этот NPC постоянно грызет ногти от нервозности", "У этого NPC есть характерная племенная татуировка",
        "У этого НПС длинные, заплетенные в косы волосы", "Дыхание этого NPC видно так, как будто вокруг всегда холодный зимний день",
        "У этого NPC очень длинные, очень острые ногти", "У этого NPC пронзительные глаза", "Этот NPC всегда пахнет выпеченным хлебом",
        "Этот NPC тощий как жердь", "Этот NPC исключительно высок для своего вида",
        "У этого NPC прядь волос всегда торчит торчком, как бы персонаж ни пытался ее удержать",
        "Этот NPC всегда говорит сквозь стиснутые зубы", "У этого NPC большой нос картошкой",
        "У этого NPC на лбу татуировка в виде глаза", "У этого NPC ленивый глаз", "Этот NPC невероятно привлекателен", "У этого NPC костлявое телосложение",
        "У этого NPC есть татуировка в виде змеи", "У этого NPC темные глаза", "От этого NPC всегда исходит сильный запах духов",
        "У этого NPC крупное и широкое телосложение", "Этот NPC говорит очень медленно", "Этот NPC всегда пахнет чесноком",
        "Этот NPC носит выцветшую, залатанную одежду", "Этот NPC, похоже, напоминает кого-то из знатной семьи аристократов, даже слишком",
        "Похоже, этот NPC мог бы отжать от груди любимую корову фермера, если бы захотел", "У этого NPC яркие, блестящие глаза",
        "У этого NPC есть татуировка в виде стрелы", "Этот NPC всегда одет опрятно и прилично", "Этот NPC всегда пахнет мятой",
        "У этого NPC усы в форме руля", "У этого NPC есть характерная татуировка банды",
        "У этого NPC витилиго, при котором на различных (или зеркальных) участках тела отсутствует меланин", "У этого NPC гетерохромия (глаза разного цвета)",
        "У этого NPC на руках всегда пятна чернил", "Этот NPC чрезвычайно волосат", "У этого NPC все зубы заменены на деревянные",
        "Этот NPC чудовищно толстый", "У этого NPC волосы двух разных цветов", "У этого NPC бегаюшие глаза",
        "Этот NPC всегда пахнет трубочным табаком", "У этого NPC очень мало волос на теле", "Этот NPC худой и жилистый",
        "У этого NPC есть вставной зуб",
        "Этот NPC, должно быть, проводит больше времени перед зеркалом, чем спит. На его лице нет ни единого изъяна или видимой морщинки, нет ни улыбки, ни хмурой линии",
        "У этого NPC далекие глаза", "Этот NPC носит повязку на одном глазу", "У этого NPC было изношенное, усталое лицо",
        "На одной стороне лица были искусно размазаны мазки грязи", "Этот NPC сильно хромает", "У этого NPC есть характерная криминальная татуировка",
        "Этот NPC — альбинос (отсутствует пигментация кожи, меха, перьев или чешуи)", "У этого NPC старомодное чувство стиля",
        "Этот NPC всегда жуёт кусок какого-то корня неизвестных свойств", "Этот NPC имеет острые зубы", "У этого NPC крючковатый нос",
        "У этого NPC ужасное зрение, и он не может видеть без очков", "Уши этого NPC растянуты/низко висят из-за ношения очень тяжелых/модифицирующих серег" };

        private List<string> _behavior = new List<string>() { "Этот NPC очень религиозен", "Этот NPC время от времени тайком отпивает из кожаной фляжки",
        "Этот NPC слишком, слишком, слишком часто чмокает губами", "Этот NPC начинает смеяться ни с того ни с сего", "Этот NPC — моряк на берегу",
        "Этот NPC любит садоводство", "У этого NPC есть любимая собака, которая следует за ними повсюду", "NPC пишет интересные истории об окружении",
        "Этот NPC только что переехал в город из далекой страны, и все думают, что он не знает местного языка на самом деле, они просто не любят общаться",
        "Этот NPC родился в далеком городе", "Этот NPC абсолютно одержим добычей пропитания",
        "У этого NPC странный акцент, и он отказывается рассказывать группе, где он его подхватил",
        "Этот NPC прочитал все книги в городе, но никогда не выезжал за его пределы", "Этот NPC известен по всему региону своей честностью",
        "Этот NPC обладает сверхъестественной способностью предсказывать погоду", "Рецепт вяленого мяса этого NPC — гордость города",
        "Этот NPC выглядит суровым и разговаривает насмешливым голосом, но на самом деле он добросердечен и щедр",
        "У этого NPC девять детей, и еще один на подходе", "У этого NPC хороший вкус",
        "У этого NPC обсессивно-компульсивное расстройство, и он постоянно пытается что-то организовать",
        "У этого NPC нет чувства направления, и он всегда идет не в ту сторону",
        "Этот NPC зациклен на обладании новейшими передовыми гаджетами, многие из которых не работают так, как обещают",
        "Этот NPC, кажется, никогда не обращает внимания на то, что ему говорят, но может слово в слово повторить все, что ему/ей говорят",
        "У этого NPC есть список пожизненных наблюдений за птицами, и он бросит все, чтобы увидеть птицу, которой нет в списке",
        "Этот NPC Всякий раз, когда кто-то меняет тему разговора, он говорит: Угу, Ну ладно, прежде чем вернуться к тому, о чем говорил",
        "Этот NPC пересыпает свою речь словами из других языков", "Этот NPC обычно насвистывает мелодию, своего рода личную песню",
        "Этот NPC предпочитает, чтобы он был единственным в комнате, держащим в руках острый инструмент или оружие, и будет настаивать на том, " +
        "чтобы сам брался за все, для чего требуется нож", "В этом NPC есть немного крови сатира, поэтому при разговоре у него козлиный призвук",
        "Этот NPC чрезмерно скрытен в отношении своих книг/папок, даже если они не представляют особой важности, как, например, его дневник",
        "Этот NPC никогда не ругается", "Этот NPC ругается через предложение",
        "Этот NPC предполагает худшее из любой ситуации, с которой ему приходится сталкиваться, даже что-то столь приятное, как пикник, может иметь " +
        "катастрофические последствия в его глазах", "Этот NPC замолкает, когда видит кого-то хоть немного привлекательного",
        "Этот NPC неустанно болтает, чтобы заполнить неловкую тишину, даже если ему самому хочется заткнуться, рядом с любым, кого он находит " +
        "хоть немного привлекательным", "Этот NPC постоянно вспоминает золотые века или старые добрые времена, даже если период, на который он намекает," +
        " был не таким уж замечательным", "Этот NPC боится крови и падает в обморок при ее виде",
        "Этот NPC должен носить определенное украшение или одежду, чтобы чувствовать себя комфортно и безопасно",
        "Этот NPC комментирует вещи, которые он наблюдает в других, вслух",
        "Этот NPC обожает сплетни и любую пикантную информацию или просто проявляет постоянное любопытство к чужим делам",
        "Этот NPC имеет привычку разглашать секреты, как свои, так и чужие, хотя и не намеренно",
        "Этот NPC тает и воркует при виде любого хотя бы отдаленно милого животного, от кошек и собак до смертоносных ",
        "Этот NPC любит слушать себя и всегда находит способ вставить в разговор свои личные истории",
        "Этот NPC любит слушать людей, говорящих о том, что их интересует, и даже если предмет обсуждения их не особенно интересует, " +
        "он будет задавать уточняющие вопросы", "Этот NPC страдает нездоровой страстью к чаю и выпивает его неприемлемое количество каждый день",
        "Этот NPC грамманаци", "Этот NPC часто теряет ход мыслей и имеет тенденцию быстро переходить от одной темы к другой",
        "Этот NPC рассказывает неуместные шутки в САМОЕ НЕУДАЧНОЕ время", "У этого NPC есть интересная история на все случаи жизни, " +
        "Иногда эти истории имеют тенденцию тянуться слишком долго", "Этот NPC не выносит океан или любой другой водоем они СМЕРТЕЛЬНО боится открытого моря",
        "Этот NPC очень религиозен и обычно любит украшать свою одежду символами своего Бога/Богини",
        "Этот NPC хочет, чтобы все думали, что он интеллектуал иногда он используют громкие слова, чтобы казаться умными, но не всегда знают, что они имеют в виду",
        "Этот NPC очень пуглив и может легко испугаться громких звуков или кого-то, идущего сзади",
        "Этот NPC звучит мило, но на самом деле он очень пассивно-агрессивен и осуждающ ",
        "У этого NPC есть хобби, о котором он с радостью вам расскажет, если вы проявите хотя бы малейший намёк на интерес",
        "Этот NPC никогда не сидит без дела; он не может просто сидеть и ничего не делать", "Этот NPC параноик, почти от всего и всех",
        "Этот NPC терпелив — слишком терпелив для собственного блага", "Этот NPC — чистюля, он любит убираться",
        "Этот NPC любит размышлять над важными вопросами: кто мы и почему мы здесь?",
        "Этот NPC любит готовить и всегда предложит еду своим гостям, даже если они откажутся",
        "Этот NPC увлечен френологией и отчаянно хочет измерить голову каждого существа, которое встретит",
        "NPC груб и молчалив, за исключением случаев, когда разговаривает с детьми, поскольку они напоминают ему его собственных детей",
        "NPC не может справиться с алкоголизмом", "NPC не выносит своей неправоты и попытается уничтожить все доводы, которые могли бы им это показать",
        "NPC постоянно нервничает и что-то делает руками: заплетает волосы в косички, барабанит пальцами по столу, теребит подол рубашки",
        "NPC — откровенный флирт, но замрет, если к нему подойти первым", "Этот NPC выглядит уставшим, говорит и ходит медленно, словно в спячке",
        "Этот NPC пытается дать PC советы в форме поговорок из его родной страны", "Этот NPC склонен к мечтаниям",
        "Этот NPC не любит насилие, но понимает, что некоторые проблемы не могут быть решены другим способом",
        "Этот NPC не доверяет всем и вся и приятно удивляется, когда что-то происходит именно так, как было обещано или обговорено",
        "Этот NPC не верит в ксеносов", "NPC постоянно пытаются подцепить волшебные грибы и пытаются скрыть этот факт",
        "Этот NPC испытывает сильное недоверие к городской страже", "Этот NPC беспокойный и часто смотрит на небо или потолок в поисках монстров",
        "Этот NPC воспринимает любую новую информацию как потенциальный обман", "Этот NPC быстро ввязывается в драки, чтобы проверить свои силы",
        "Этот NPC не будет доволен, если он не окажется самым экстравагантно одетым человеком в комнате",
        "Этот NPC часто говорит загадками или стихами, и разобраться во всем этом может быть довольно сложно",
        "Этот NPC неукоснительно соблюдает правила, даже если это доставляет неудобства ему и всем вокруг",
        "Этот NPC разговаривает со всеми спокойным, но снисходительным голосом, словно с ребенком",
        "Этот NPC относится к своим инструментам/транспортному средству с большей любовью и привязанностью, чем к своей собственной семье",
        "Этот NPC дает всем прозвища и отказывается узнавать их настоящие имена",
        "Этот NPC всегда носит еду на палочке, и если его попросить, он с радостью порекомендует сомнительную лавку, в которой он ее купил",
        "Этот NPC упорно носит слишком тяжелые для него вещи, или заботится о хрупких предметах, которые он неизменно ломает, или присматривает " +
        "за ценностями, которые он обязательно потеряет", "Этот NPC спит весь день и не спит всю ночь" };

        private List<string> _nature = new List<string>() {"Склонность к абьюзу — вы физически и/или психологически жестоки ко всем или только к сознательно выбранной жертве", "Хорошее чувство юмора",
        "Любите пошутить в любой ситуации", "Трудоголизм", "Безмятежность", "Верность", "Честность", "Лень", "Покорность", "Лицемерие", "Оптимизм",
        "Доброта", "Вы обожаете всё, что вызывает быстрое привыкание, например, зависимы от азартных игр, наркотиков или секса", "Великодушие", "Наивность",
        "Щедрость", "Открытость", "Мания величия", "Пластичность", "Безрассудство","Целеустремлённость", "Артистичность",
        "Отсутствие чувства юмора", "Развитая эмпатия", "Благожелательность", "Альтруизм", "Перфекционизм", "Склонность к мученичеству",
        "Жестокость", "Надоедливость", "Увлечённость любым делом, которым вы заняты в данный момент", "Склонность к предательству",
        "Безжалостность", "Храбрость", "Сильная воля", "Склонность к беспокойству по любому поводу", "Восторженность", "Доверчивость",
        "Поверхностность", "Хвастовство", "Всепрощение", "Галантность", "Сверхрациональность", "Нетерпеливость", "Нетерпимость", "Фиксация на чём-либо",
        "Героизм", "Гостеприимство", "Наглость", "Свободолюбие", "Дисциплинированность", "Идеализм", "Добропорядочность", "Законопослушность",
        "Склонность к болтовне", "Обаятельность", "Суеверность", "Общительность", "Избалованность", "Высокомерность", "Зрелость", "Интеллигентность",
        "Инфантильность", "Робость", "Неподкупность", "Искренность", "Гедонизм", "Сверхэмоциональность", "Строгость", "Несгибаемость", "Инициативность",
        "Красноречивость", "Неуверенность", "Рассеянность", "Нежность", "Бестактность", "Независимость", "Чёрствость, безэмоциональность", "Немногословность",
        "Выученная беспомощность", "Склонность к лидерству", "Стремление взять власть в свои руки", "Косноязычность", "Любвеобильность", "Трусость", "Любопытство",
        "Раздражительность", "Настойчивость", "Завистливость", "Невозмутимость", "Угрюмость", "Гордыня","Склонна к розыгрышам, иногда жестоким",
        "Мечтательность", "Бесцельность","Предсказуемость", "Привередливость",
        "Склонность лгать в любой ситуации, даже если это не несёт прямой выгоды, просто от любви к процессу", "Пессимизм", "Миролюбивость",
        "Вы никогда и ни при каких обстоятельствах не можете отказать в любой помощи",  };

        private List<string> _secrets = new List<string>() { "Дворянин инкогнито ", "Бывший представитель тайной организации  ",
        "Текущий представитель тайной организации  ", "Любовник (бывший?) кого-то влиятельного ",
        "Друг или родственник врага партии ", "Не помнит своего прошлого ", "Экстремист-террорист ", "Поклоняется темному божеству ",
        "Работает на мафию ", "В бегах от мафии ", "Опозорился ", "Совершил преступление ", "Неизлечимо болен ",
        "Шпионит на противника партии ", "Шпионит на могущественную организацию ", "Неприятная привычка, почти мания ",
        "Тяжелое психическое расстройство ", "Некогда или негде знаменит ", "Обладатель чудесной силы или умения ", "Мутант", };

        public string GetPhysicalTrait() => _physicalTraits[RandomFromList(_physicalTraits.Count)];

        public string GetBehaivor() => _behavior[RandomFromList(_behavior.Count)];

        public string GetNature() => _nature[RandomFromList(_nature.Count)];

        public string GetSecret() => _secrets[RandomFromList(_secrets.Count)];

        private int RandomFromList(int maxValue)
        {
            System.Random random = new System.Random();
            return random.Next(maxValue);
        }
    }
}


