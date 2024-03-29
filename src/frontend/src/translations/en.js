export const ENGLISH_TRANSLATIONS = {
  tabs: {
    start: {
      name: 'Getting started',
      createProject: 'Create project',
      openProject: 'Open project'
    },
    template: {
      name: 'Template',
      author: 'Author',
      licence: 'Licence'
    },
    settings: {
      name: 'Site settings',
      main: 'Main settings',
      advanced: 'Advanced settings',
      icon: 'Icon',
      siteTitle: 'Site title',
      shortDescription: 'Short description',
      keywords: 'Keywords',
      templateSection: 'Template section names',
      templateFields: {
        1: 'Main menu title',
        2: 'Subtitle',
        3: 'Aside menu title',
        4: 'About title',
        5: 'Short about site',
        6: 'Footer content'
      },
      userScripts: 'User scripts',
      imageFormatError: 'Image must be JPG or PNG format!',
      iconFormatError: 'Icon picture must be ICO, JPG or PNG format!',
      iconSizeError: 'Icon picture size can not exceed {size}!'
    },
    pages: {
      name: 'Pages',
      defaultPageName: 'Page',
      editor: 'Editor',
      settings: 'Settings',
      url: 'Custom url',
      title: 'Custom title',
      description: 'Custom description',
      keywords: 'Custom keywords',
      existsError: 'Page with this URL already exists',
      errorLength:
        'The string length must be between {from} and {to} characters',
      inMainMenu: 'In main menu',
      inAsideMenu: 'In aside menu'
    },
    blocks: {
      name: 'Blocks',
      defaultBlockName: 'Block',
      title: 'Block title',
      description:
        'The area on the side of the content for placing categories, archive links, tags, and other information. Located on all pages.'
    },
    publish: {
      name: 'Publication',
      log: {
        name: 'Log'
      },
      deploy: 'Deploy',
      deployed: 'Site was published successfully!',
      ways: {
        details: 'To learn more',
        ftp: {
          name: 'FTP',
          description: 'Download archive with your website and upload to your server.',
          link: 'https://en.wikipedia.org/wiki/File_Transfer_Protocol',
          host: 'Host',
          port: 'Port',
          login: 'Login',
          password: 'Password',
          folder: 'Site folder on server'
        },
        github: {
          name: 'Github Pages',
          description:
            'GitHub Pages is a static site hosting service that takes HTML, CSS, and JavaScript files straight from a repository on GitHub.',
          link: 'https://pages.github.com/'
        },
        vercel: {
          name: 'Vercel',
          description:
            'Vercel is an all-in-one platform for automating modern web projects.',
          link: 'https://vercel.com/'
        }
      }
    }
  },
  feedback: {
    title: 'Feedback',
    name: 'Name',
    email: 'Email',
    message: 'Message',
    send: 'Send',
    thanks: 'Thank you for your feedback!'
  },
  about: {
    title: 'Static sites generator with content management system',
    shortTitle: 'About',
    getStarted: 'Get started',
    vantages: [
      {
        title: 'Templates',
        desc: 'Choose templates from gallery or create your own.'
      },
      {
        title: 'Editor',
        desc:
          'Сreate pages and blocks with rich-text editing and real-time preview.'
      },
      {
        title: 'Deploy',
        desc: 'Publish on FTP or Vercel.'
      }
    ]
  },
  notfound: {
    title: 'Page not found 🙄'
  },
  user: {
    register: 'Sign up',
    login: 'Sign in',
    logout: 'Log out',
    male: 'Male',
    female: 'Female',
    projects: 'Projects',
    settings: 'Settings',
    username: 'Username',
    delete: 'Delete account',
    deleteWarning:
      "Are you sure you want to delete your account? You can't undo this.",
    confirm:
      'We just sent you a link to activate your account. Check your email <b>{email}</b>'
  },
  status: {
    success: 'Success',
    error: 'Error',
    info: 'Information',
    warning: 'Warning',
    authError: 'An error occurred during authorization',
    registerError: 'An error occurred during registration'
  },
  validation: {
    required: 'Required field',
    notEmpty: 'Field cannot be empty',
    email: 'The format does not match the email address — example@mail.com',
    maxLength: 'Limit on {count} characters',
    minLength: 'Min lenght on {count} characters'
  },
  siteActions: {
    view: 'Open site',
    generate: 'Generate site'
  },
  common: {
    donate: 'I ❤️ TextBooker',
    help: 'Help',
    docs: 'Docs',
    empty: 'Empty',
    add: 'Add',
    update: 'Update',
    edit: 'Edit',
    delete: 'Delete',
    yes: 'Yes',
    no: 'No',
    save: 'Save',
    deleteWarning: 'The action is irreversible. Delete it?',
    preview: 'Preview',
    htmlEditor: 'HTML editor',
    wysiwygEditor: 'Wysiwyg editor',
    email: 'Email',
    password: 'Password',
    continue: 'Continue',
    metrics: 'We does not track you or collect analytics.',
    title: 'Title',
    description: 'Description',
    updatedOn: 'Updated on',
    createdOn: 'Created on',
    search: 'Search',
    enabled: 'Enabled',
    disabled: 'Disabled',
    close: 'Close',
    select: 'Select',
    lang: 'Language',
    width: 'Width',
    height: 'Height',
    theme: 'Theme',
    download: 'Download',
  },
  twitter: {
    feedTitle: 'Twitter feed',
    buttonTitle: 'Twitter button',
    url: 'Twitter URL',
    invalidUrl: 'Incorrect Twitter url'
  }
}
