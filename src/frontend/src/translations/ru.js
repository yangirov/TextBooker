export const RUSSIAN_TRANSLATIONS = {
  tabs: {
    start: {
      name: 'Начало',
      createProject: 'Создать проект',
      openProject: 'Открыть проект'
    },
    template: {
      name: 'Шаблон',
      author: 'Автор',
      licence: 'Лицензия'
    },
    settings: {
      name: 'Параметры сайта',
      main: 'Основные настройки',
      advanced: 'Расширенные настройки',
      icon: 'Иконка',
      siteTitle: 'Заголовок сайта',
      shortDescription: 'Короткое описание сайта',
      keywords: 'Ключевые слова (СЕО)',
      customCode: 'Пользовательский код',
      templateSection: 'Поля шаблона',
      templateFields: {
        1: 'Заголовок главного меню',
        2: 'Подзаголовок',
        3: 'Заголовок бокового меню',
        4: 'Заголовок о сайте',
        5: 'Коротко о сайте',
        6: 'Подвал'
      },
      userScripts: 'Пользовательский код',
      iconFormatError: 'Формат иконки должен быть ICO, JPG или PNG!',
      iconSizeError: 'Размер иконки должен быть меньше 100 Кб!'
    },
    pages: {
      name: 'Страницы',
      defaultPageName: 'Страница',
      editor: 'Редактор',
      settings: 'Параметры страницы',
      url: 'Свой адрес страницы',
      title: 'Свой заголовок',
      description: 'Своё описание',
      keywords: 'Свои ключевые слова'
    },
    blocks: {
      name: 'Блоки',
      defaultBlockName: 'Блок',
      title: 'Заголовок блока',
      description:
        'Область сбоку от контента для размещения рубрик, ссылок на архив, меток и другой информации. Располагается на всех страницах.'
    },
    publish: {
      name: 'Публикация в интернете',
      log: 'Лог',
      deploy: 'Опубликовать',
      ways: {
        details: 'Узнать больше',
        github: {
          description:
            'Cтатический хостинг сайтов, в котором HTML, CSS и JavaScript файлы берутся прямо из репозитория на GitHub.',
          link: 'https://pages.github.com/'
        },
        netlify: {
          description:
            'Универсальная платформа для автоматизации современных веб-проектов.',
          link: 'https://www.netlify.com/'
        }
      }
    }
  },
  feedback: {
    title: 'Обратная связь',
    name: 'Ваше имя',
    email: 'Электронная почта',
    message: 'Сообщение',
    send: 'Отправить',
    thanks: 'Спасибо за обратную связь!'
  },
  about: {
    title:
      'Генератор статических сайтов и системой управления контентом <br>с открытым исходным кодом',
    shortTitle: 'О сервисе',
    getStarted: 'Начать',
    vantages: [
      {
        title: 'Шаблоны',
        desc: 'Выберите шаблон из галереи или создайте свой'
      },
      {
        title: 'Редактор',
        desc:
          'Создавайте страницы и блоки с помощью удобного редактора с функцией предпросмотра'
      },
      {
        title: 'Публикация в интернете',
        desc: 'Публикуйте сайты на FTP, Github Pages или Netlify.'
      }
    ]
  },
  notfound: {
    title: 'Страница не найдена 🙄'
  },
  user: {
    register: 'Регистрация',
    login: 'Войти',
    logout: 'Выйти',
    male: 'Мужской',
    female: 'Женский',
    projects: 'Проекты',
    settings: 'Настройки',
    username: 'Никнейм',
    delete: 'Удалить аккаунт',
    deleteWarning:
      'Вы уверены что хотите удалить свой аккаунт? Действие нельзя отменить.',
    confirm:
      'Мы только что отправили вам ссылку для активации аккаунта. Проверьте свою почту <b>{email}</b>'
  },
  status: {
    success: 'Успешно',
    error: 'Ошибка',
    info: 'Информация',
    warning: 'Предупреждение',
    authError: 'Возникла ошибка при авторизации',
    registerError: 'Возникла ошибка при регистрации'
  },
  validation: {
    required: 'Обязательное поле',
    notEmpty: 'Поле не может быть пустым',
    email: 'Формат не соответсвует email адресу — example@mail.com',
    maxLength: 'Ограничение на {count} символов'
  },
  siteActions: {
    view: 'Открыть сайт',
    generate: 'Генерировать сайт'
  },
  common: {
    donate: 'Я ❤️ TextBooker',
    help: 'Руководство',
    docs: 'Документация',
    empty: 'Пусто',
    add: 'Добавить',
    update: 'Обновить',
    edit: 'Редактировать',
    delete: 'Удалить',
    yes: 'Да',
    no: 'Нет',
    save: 'Сохранить',
    deleteWarning: 'Действие необратимо. Удалить?',
    preview: 'Предпросмотр',
    htmlEditor: 'HTML-код',
    wysiwygEditor: 'Визуальный редактор',
    field: 'Field',
    value: 'Value',
    email: 'Электронная почта',
    password: 'Пароль',
    continue: 'Продолжить',
    metrics: 'Мы не следим за вами и не собираем никаких данных.',
    title: 'Название',
    description: 'Описание',
    updatedOn: 'Дата обновления',
    createdOn: 'Дата создания',
    search: 'Поиск',
    enabled: 'Включен',
    disabled: 'Выключен',
    close: 'Закрыть',
    select: 'Выбрать'
  }
}
